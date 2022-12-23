import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { DonorsRoutingModule } from './donors-routing.module';
import { DonarsDisplayComponent } from './components/donars-display/donars-display.component';
import { DonarCardComponent } from './components/donar-card/donar-card.component';
import { PrimeNgAppModule } from '../../primng.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { DonorService } from './services/donor.service';
import { DonorDetailComponent } from './components/donor-detail/donor-detail.component';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    DonorsRoutingModule,
    PrimeNgAppModule,
    ReactiveFormsModule,
    SharedModule
  ],
  declarations: [
    DonarsDisplayComponent,
    DonarCardComponent,
    DonorDetailComponent,
  ],
  providers: [
    DonorService
  ]
})
export class DonorsModule { }
