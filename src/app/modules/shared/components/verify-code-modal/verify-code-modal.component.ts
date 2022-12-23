import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { RegisterDonorModel } from '../../../auth/models/register-donor.model';
import { AuthService } from '../../../auth/services/auth.service';

@Component({
  selector: 'app-verify-code-modal',
  templateUrl: './verify-code-modal.component.html',
  styleUrls: ['./verify-code-modal.component.css']
})
export class VerifyCodeModalComponent implements OnInit {

  errVerificationCode = false;
  submitted = false;
  userModel: RegisterDonorModel;
  verifyForm: FormGroup = new FormGroup({});

  constructor(
    private modalCtrl: ModalController,
    private authService: AuthService,
    private fb: FormBuilder) {}

  ngOnInit(): void {
    this.verifyForm = this.fb.group({
      code: [null, Validators.required],
    });
  }

  get verfiyFormControls() {
    return this.verifyForm.controls;
  }

  onCancel(){
    return this.modalCtrl.dismiss({
      isVerified: false
    });
  }

  async onSave() {
    this.submitted = true;
    this.errVerificationCode = false;
    if (this.verifyForm.valid) {
      
      let isVerified = await this.authService.verifyCode(this.verifyForm.get("code")?.value, this.userModel.birthDate);
      if(isVerified){
        return this.modalCtrl.dismiss({
          isVerified: true
        });
      }

      this.errVerificationCode = true;
    }
  }

}
