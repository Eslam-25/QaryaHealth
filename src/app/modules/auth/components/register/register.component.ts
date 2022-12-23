import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { Gender } from '../../../../config/enums/gender.enum';
import { Role } from '../../../../config/enums/role.enum';
import { BloodTypeModel, BloodTypes } from '../../../../config/models/blood-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { DatePickerArabicComponent } from '../../../shared/components/date-picker-arabic/date-picker-arabic.component';
import { VerifyCodeModalComponent } from '../../../shared/components/verify-code-modal/verify-code-modal.component';
import { RegisterDonorModel } from '../../models/register-donor.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  submitted = false;
  userModel: RegisterDonorModel = { gender: Gender.Male, role: Role.Donar } as RegisterDonorModel;

  userForm: FormGroup = new FormGroup({});
  bloodTypes: BloodTypeModel[] = BloodTypes();

  constructor(
    private fb: FormBuilder,
    private toastService: ToastService,
    private modalCtrl: ModalController,
    private loaderService: LoaderService,
    private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
    this.prepareFormGroup();
  }

  prepareFormGroup() {
    this.userForm = this.fb.group({
      name: [this.userModel.name, Validators.required],
      address: [this.userModel.address, Validators.required],
      phoneNumber: [this.userModel.phoneNumber, Validators.required],
      email: [this.userModel.email, Validators.required],
      password: [this.userModel.password, Validators.required],
      bloodType: [this.userModel.bloodType, Validators.required],
      gender: [this.userModel.gender, Validators.required],
    });
  }

  get userFormControls() {
    return this.userForm.controls;
  }

  mapModelFromUI() {
    this.userModel.name = this.userForm.get("name")?.value;
    this.userModel.address = this.userForm.get("address")?.value;
    this.userModel.phoneNumber = this.userForm.get("phoneNumber")?.value;
    this.userModel.email = this.userForm.get("email")?.value;
    this.userModel.password = this.userForm.get("password")?.value;
    this.userModel.bloodType = this.userForm.get("bloodType")?.value;
    this.userModel.gender = this.userForm.get("gender")?.value;
    this.userModel.readyToDonor = false;
  }

  async openDatePicker(typeId){
    const modal =  await this.modalCtrl.create({
      component: DatePickerArabicComponent,
      backdropDismiss: false
    });

    modal.present();
    const { data } = await modal.onDidDismiss();
    if(typeId == 0)
      this.userModel.birthDate = new Date(data?.selectedDate);
    else
      this.userModel.lastDonationDate = new Date(data?.selectedDate);
  }

  displayDate(date: Date){
    return date?.toLocaleDateString("en-GB");
  }

  async openModalToVerifyCode() : Promise<boolean>{
    const modal =  await this.modalCtrl.create({
      component: VerifyCodeModalComponent,
      componentProps: {
        userModel: this.userModel
      }
    });

    modal.present();
    const { data, role } = await modal.onDidDismiss();
    return data?.isVerified;
  }

  async onSave() {

    this.submitted = true;
    if (this.userForm.valid) {

      this.mapModelFromUI();

      this.loaderService.showLoader();
      let emailSent = await this.authService.sendEmail(this.userModel.name, this.userModel.email, this.userModel.birthDate);
      this.loaderService.hideLoader();

      if (emailSent) {

        let codeIsVerified = await this.openModalToVerifyCode();
        if (codeIsVerified) {
          await this.onRegister();
        }

      }else{
        await this.toastService.show("لقد حدث خطأ حاول مرة اخري");
      }
    }
  }

  async onRegister() {

    this.loaderService.showLoader();
    let isAdded = await this.authService.register(this.userModel);
    
    if (isAdded) {
      this.userForm.reset();
      await this.toastService.show("تمت الاضافة بنجاح");
      
      this.router.navigateByUrl("/home").then(() => location.reload());
    } else {
      await this.toastService.show("لقد حدث خطأ حاول مرة اخري");
      this.loaderService.hideLoader();
    }
  }

}
