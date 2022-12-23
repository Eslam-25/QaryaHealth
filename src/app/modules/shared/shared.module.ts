import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { PrimeNgAppModule } from '../../primng.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VerifyCodeModalComponent } from './components/verify-code-modal/verify-code-modal.component';
import { NoItemsComponent } from './components/no-items/no-items.component';
import { DatePickerArabicComponent } from './components/date-picker-arabic/date-picker-arabic.component';

@NgModule({
  imports: [
    CommonModule,
    IonicModule,
    PrimeNgAppModule,
    ReactiveFormsModule,
    FormsModule
  ],
  declarations: [
    VerifyCodeModalComponent,
    NoItemsComponent,
    DatePickerArabicComponent
  ],
  exports: [
    NoItemsComponent
  ]
})
export class SharedModule { }
