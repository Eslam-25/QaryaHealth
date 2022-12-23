import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { VerifyCodeModalComponent } from '../../../shared/components/verify-code-modal/verify-code-modal.component';
import { ForgetPasswordModel } from '../../models/forget-password.model';
import { RegisterDonorModel } from '../../models/register-donor.model';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent implements OnInit {

  isLoading = false;
  submitted = false;
  showUpdatePassword = false;
  forgetPasswordModel: ForgetPasswordModel = {} as ForgetPasswordModel;
  userModel: RegisterDonorModel = {} as RegisterDonorModel;
  forgetPasswordForm: FormGroup = new FormGroup({});
  newPasswordForm: FormGroup = new FormGroup({});

  constructor(
    private fb: FormBuilder,
    private loaderService: LoaderService,
    private authService: AuthService,
    private toastService: ToastService,
    private modalCtrl: ModalController,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.prepareFormGroup();
  }

  prepareFormGroup() {
    this.forgetPasswordForm = this.fb.group({
      email: ['', Validators.required],
    });

    this.newPasswordForm = this.fb.group({
      password: ['', Validators.required],
    });
  }

  get forgetPasswordControls() {
    return this.forgetPasswordForm.controls;
  }

  get newPasswordControls() {
    return this.newPasswordForm.controls;
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
    if (this.forgetPasswordForm.valid) {
      this.loaderService.showLoader();
      let user = await this.authService.getUserByEmail(this.forgetPasswordForm.get("email")?.value);

      if (user === null) {
        this.toastService.show("عفوا هذا الايميل غير موجود");
      } else {
        this.userModel = user;
        let emailSent = await this.authService.sendEmail(user.name, user.email, user.birthDate);
        this.loaderService.hideLoader();
        if (emailSent) {
          let codeIsVerified = await this.openModalToVerifyCode();
          if (codeIsVerified) {
            this.showUpdatePassword = true;
          }

        }else{
          this.toastService.showErrorMsg();
        }
      }
    }
  }


  async onUpdatePassword(){
    this.submitted = true;
    if (this.forgetPasswordForm.valid) {
      this.loaderService.showLoader();
      this.userModel.password = this.newPasswordForm.get("password")?.value
      let isUpdated = await this.authService.updateUser(this.userModel);
      this.loaderService.hideLoader();
      if (isUpdated) {
        this.toastService.show("تم التعديل بنجاح");
        this.router.navigateByUrl("/auth/login");
      }else{
        this.toastService.showErrorMsg();
      }
    }
  }
}
