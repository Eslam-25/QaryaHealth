import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { PrimeNgAppModule } from '../../primng.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';
import { AuthService } from './services/auth.service';
import { SharedModule } from '../shared/shared.module';
import { DonorService } from '../donors/services/donor.service';
import { MedicalStaffService } from '../medical-staff/services/medical-staff.service';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    PrimeNgAppModule,
    ReactiveFormsModule,
    AuthRoutingModule,
    SharedModule,
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForgetPasswordComponent
  ],
  providers: [
    AuthService,
    DonorService,
    MedicalStaffService
  ]
})
export class AuthModule { }
