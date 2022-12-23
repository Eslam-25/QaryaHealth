import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { PrimeNgAppModule } from '../../primng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { MedicalStaffRoutingModule } from './medical-staff-routing.module';
import { StaffDisplayComponent } from './components/staff-display/staff-display.component';
import { StaffCardComponent } from './components/staff-card/staff-card.component';
import { StaffFormComponent } from './components/staff-form/staff-form.component';
import { MedicalStaffService } from './services/medical-staff.service';
import { StaffDetailComponent } from './components/staff-detail/staff-detail.component';
import { StaffServicesComponent } from './components/staff-services/staff-services.component';
import { StaffServiceFormComponent } from './components/staff-service-form/staff-service-form.component';
import { MedicalStaffServiceTypeService } from './services/medical-staff-service-type.service';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    PrimeNgAppModule,
    ReactiveFormsModule,
    SharedModule,
    FormsModule,
    MedicalStaffRoutingModule
  ],
  declarations: [
    StaffDisplayComponent,
       StaffCardComponent,
       StaffFormComponent,
       StaffDetailComponent,
       StaffServicesComponent,
       StaffServiceFormComponent
  ],
  providers: [
    MedicalStaffService,
    MedicalStaffServiceTypeService
  ]
})
export class MedicalStaffModule { }
