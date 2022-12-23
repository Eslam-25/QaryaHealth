import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ActionSheetController } from '@ionic/angular';
import { environment } from '../../../../../environments/environment';
import { getMedicalStaffByType, MedicalStaffTypeModel } from '../../../../config/models/medical-staff-types.model';
import { LoaderService } from '../../../../config/services/loader.service';
import { MedicalStaffModel } from '../../models/medical-staff.model';
import { MedicalStaffService } from '../../services/medical-staff.service';

@Component({
  selector: 'app-staff-detail',
  templateUrl: './staff-detail.component.html',
  styleUrls: ['./staff-detail.component.css']
})
export class StaffDetailComponent implements OnInit {

  isLoaded = false;
  medicalStaffType: MedicalStaffTypeModel;
  medicalStaff: MedicalStaffModel = {} as MedicalStaffModel;

  constructor(
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private staffService: MedicalStaffService,
    public actionSheetCtrl: ActionSheetController,
    ) { }

  ngOnInit(): void {
    this.loaderService.showLoader();
    this.activatedRoute.params.subscribe(async params => {
      await this.getStaffData(params.id);
      this.medicalStaffType = getMedicalStaffByType(this.medicalStaff.type);
      this.isLoaded = true;
    });
  }

  async getStaffData(userId: number) {
    this.loaderService.showLoader();

    this.medicalStaff = await this.staffService.getStaff(userId);

    this.loaderService.hideLoader();
  }

  getImagePath(){
    if(this.medicalStaff?.imagePath?.length)
      return `${environment.apiURL}${environment.imageSegment}${this.medicalStaff.imagePath}`;
      
    return "../../../../../assets/img/male.png";
  }

  getWorkingDays(workingDays: string){
    return workingDays?.split(',');
  }

  async openContact(medicalStaff: any) {
    const mode = 'ios'; // this.config.get('mode');

    const actionSheet = await this.actionSheetCtrl.create({
      header: 'اتصال ب :' + medicalStaff.name,
      buttons: [
        {
          text: `${medicalStaff.phoneNumber}`,
          icon: 'call',
          cssClass: 'call-alert-section',
          handler: () => {
            window.open('tel:' + medicalStaff.phoneNumber);
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
