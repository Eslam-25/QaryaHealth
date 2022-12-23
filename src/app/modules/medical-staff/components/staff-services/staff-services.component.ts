import { Component, OnInit } from '@angular/core';
import { AlertController, ModalController } from '@ionic/angular';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { MedicalStaffModel } from '../../models/medical-staff.model';
import { MedicalStaffServiceTypeService } from '../../services/medical-staff-service-type.service';
import { MedicalStaffService } from '../../services/medical-staff.service';
import { StaffServiceFormComponent } from '../staff-service-form/staff-service-form.component';

@Component({
  selector: 'app-staff-services',
  templateUrl: './staff-services.component.html',
  styleUrls: ['./staff-services.component.css']
})
export class StaffServicesComponent implements OnInit {

  isLoaded = false;
  medicalStaff: MedicalStaffModel = {} as MedicalStaffModel;

  constructor(
    private loaderService: LoaderService,
    private toastService: ToastService,
    private authService: AuthService,
    private staffService: MedicalStaffService,
    private staffServiceType: MedicalStaffServiceTypeService,
    private alertController: AlertController,
    private modalCtrl: ModalController
    ) { }

  async ngOnInit() {
    this.loaderService.showLoader();
    await this.getStaff();
    this.isLoaded = true;
    this.loaderService.hideLoader();
  }

  async getStaff(){
    let userId = await this.authService.userId();
    this.medicalStaff = await this.staffService.getStaff(userId);
  }

  getImagePath(){
    if(this.medicalStaff?.imagePath?.length)
      return this.medicalStaff.imagePath;
      
    return "../../../../../assets/img/male.png";
  }

  async onDelete(id: number){

    const alert = await this.alertController.create({
      header: 'حذف',
      message: 'هل تريد حذف الخدمة ؟',
      buttons: [
        {
          text: 'نعم',
          role: 'confirm',
          handler: async () => {
            this.loaderService.showLoader();
            
            let isDeleted = await this.staffServiceType.delete(id);
            if(isDeleted){
              await this.getStaff();
              this.toastService.show("تم الحذف بنجاح");
            }else{
              this.toastService.showErrorMsg();
            }

            this.loaderService.hideLoader();
          },
        },
        {
          text: 'لا',
          role: 'cancel',
        }
      ],
    });

    await alert.present();
  }

  async onOpenForm(service: any){
    const modal =  await this.modalCtrl.create({
      component: StaffServiceFormComponent,
      componentProps: {
        staffService: service,
      }
    });

    modal.present();
    let {data} = await modal.onDidDismiss();
    if(data.isSuccessfully){
      this.loaderService.showLoader();
      await this.getStaff();
      this.loaderService.hideLoader();
    }

  }

}
