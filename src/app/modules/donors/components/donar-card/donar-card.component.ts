import { Component, Input, OnInit } from '@angular/core';
import { ActionSheetController } from '@ionic/angular';
import { BloodType } from '../../../../config/enums/blood-types.enum';
import { Gender } from '../../../../config/enums/gender.enum';
import { BloodTypes } from '../../../../config/models/blood-types.model';
import { DonarModel } from '../../models/donar.model';

@Component({
  selector: 'donar-card',
  templateUrl: './donar-card.component.html',
  styleUrls: ['./donar-card.component.css']
})
export class DonarCardComponent implements OnInit {

  @Input() donar: DonarModel;
  constructor(public actionSheetCtrl: ActionSheetController) { }

  ngOnInit(): void {
  }

  getBloodType(type: BloodType){
    return BloodTypes().find(r => r.type == type)?.name;
  }

  getAge(birthDate: Date){
    return new Date().getFullYear() - new Date(birthDate).getFullYear();
  }

  getLastDoration(lastDoration: Date){
    return new Date(lastDoration).toLocaleDateString('ar-EG', {year: "numeric", month: "short"});
  }

  async openContact(donar: DonarModel) {
    const actionSheet = await this.actionSheetCtrl.create({
      header: 'اتصال بالمتبرع : ' + donar.name,
      buttons: [
        {
          text: `${donar.phoneNumber}`,
          icon: 'call',
          cssClass: 'call-alert-section',
          handler: () => {
            window.open('tel:' + donar.phoneNumber);
          }
        },
        {
          text: 'إلغاء',
          role: 'cancel'
        }
      ]
    });

    await actionSheet.present();
  }
}
