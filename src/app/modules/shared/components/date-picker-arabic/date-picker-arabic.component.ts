import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-date-picker-arabic',
  templateUrl: './date-picker-arabic.component.html',
  styleUrls: ['./date-picker-arabic.component.css']
})
export class DatePickerArabicComponent implements OnInit {

  arabicConfig = {
    "dayNames": ["الاحد", "الاثنين", "الثلاثاء", "الاربعاء", "الخميس", "الجمعة", "السبت"],
    "dayNamesMin": ["ح","اث","ث","ار","خ","ج","س"],
    "monthNames": ["يناير","فبراير","مارس","ابريل","مايو","يونيو","يوليو","اغسطس","سبتمبر","اكتوبر","نوفمبر","ديسمبر"],
    "monthNamesShort": ["يناير","فبراير","مارس","ابريل","مايو","يونيو","يوليو","اغسطس","سبتمبر","اكتوبر","نوفمبر","ديسمبر"],
  };

  constructor(
    private modalCtrl: ModalController,
    private config: PrimeNGConfig
    ) { }

  ngOnInit(): void {
    this.config.setTranslation(this.arabicConfig)
  }

  onSelectedDate(date: Date){
    return this.modalCtrl.dismiss({selectedDate: date});
  }
  
}
