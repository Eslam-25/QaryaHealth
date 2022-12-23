import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BloodTypeModel, BloodTypes } from '../../../../config/models/blood-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { DonarModel } from '../../models/donar.model';
import { DonorService } from '../../services/donor.service';
import { Storage } from '@ionic/storage';
import { DatePickerArabicComponent } from '../../../shared/components/date-picker-arabic/date-picker-arabic.component';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-donor-detail',
  templateUrl: './donor-detail.component.html',
  styleUrls: ['./donor-detail.component.css']
})
export class DonorDetailComponent implements OnInit {

  isLoaded = false;
  submitted = false;
  userModel: DonarModel = {} as DonarModel;

  userForm: FormGroup = new FormGroup({});
  bloodTypes: BloodTypeModel[] = BloodTypes();

  constructor(
    private fb: FormBuilder,
    private toastService: ToastService,
    private loaderService: LoaderService,
    private authService: AuthService,
    private modalCtrl: ModalController,
    private storage: Storage,
    private donorService: DonorService) { }

  async ngOnInit() {
    this.prepareFormGroup();
    await this.prepareDonarInfo();
    this.isLoaded = true;
  }

  async prepareDonarInfo() {
    this.userModel.id = await this.authService.userId();
    this.userModel = await this.donorService.getDonar(this.userModel.id);
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
  }

  async openDatePicker(typeId){
    const modal =  await this.modalCtrl.create({
      component: DatePickerArabicComponent,
    });

    modal.present();
    const { data } = await modal.onDidDismiss();
    if(typeId == 0)
      this.userModel.birthDate = new Date(data?.selectedDate);
    else
      this.userModel.lastDonationDate = new Date(data?.selectedDate);
  }

  displayDate(date: any){
    if(date)
      return new Date(date)?.toLocaleDateString();
  }

  async onSave() {

    this.submitted = true;
    if (this.userForm.valid) {

      this.mapModelFromUI();
      this.loaderService.showLoader();
      let isUpdated = await this.donorService.update(this.userModel);
      if (isUpdated) {
        await this.storage.set('name', this.userModel.name);
        await this.storage.set('phone', this.userModel.phoneNumber);
        await this.toastService.show("تم التعديل بنجاح");
      } else {
        await this.toastService.showErrorMsg();
      }
      this.loaderService.hideLoader();
    }
  }

}
