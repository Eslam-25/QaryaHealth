import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { HomeRoutingModule } from './home-routing.module';
import { PrimeNgAppModule } from '../../primng.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeDisplayComponent } from './components/home-display/home-display.component';
import { MedicalStaffCardComponent } from './components/medical-staff-card/medical-staff-card.component';
import { AdvImageDisplayComponent } from './components/adv-image-display/adv-image-display.component';
import { AdvImageService } from './services/adv-image.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    HomeRoutingModule,
    PrimeNgAppModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
  
    HomeDisplayComponent,
       MedicalStaffCardComponent,
       AdvImageDisplayComponent
  ],
  providers: [
    AdvImageService
  ]
})
export class HomeModule { }
