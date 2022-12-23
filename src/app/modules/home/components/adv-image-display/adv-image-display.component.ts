import { Component, OnInit } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { environment } from '../../../../../environments/environment';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { AdvImageModel } from '../../models/adv-image.model';
import { AdvImageService } from '../../services/adv-image.service';

@Component({
  selector: 'app-adv-image-display',
  templateUrl: './adv-image-display.component.html',
  styleUrls: ['./adv-image-display.component.css']
})
export class AdvImageDisplayComponent implements OnInit {

  isLoaded = false;
  images: AdvImageModel[] = [];

  constructor(
    private loaderService: LoaderService,
    private advImageService: AdvImageService,
    private authService: AuthService,
    private toastService: ToastService,
    private alertController: AlertController
  ) { }

  async ngOnInit() {
    this.loaderService.showLoader();
    this.images = await this.advImageService.getImages();
    this.isLoaded = true;
    this.loaderService.hideLoader();
  }

  getImage(imagePath: string){
    return `${environment.apiURL}${environment.imageSegment}${imagePath}`;
  }

  async onUploadImage(event: any) {
    const file: File = event.target.files[0];
    const formData = new FormData();
    formData.append('file', file, file.name);

    this.loaderService.showLoader();
    let uploadedImagePath = await this.advImageService.uploadImage(formData);
    if(uploadedImagePath){
      let advImageModel = {} as AdvImageModel;

      advImageModel.isActive = true;
      advImageModel.imagePath = uploadedImagePath?.fileName;
      advImageModel.userId = await this.authService.userId();
      await this.onAdd(advImageModel);      

    }else{
      this.toastService.showErrorMsg();
    }

    this.loaderService.hideLoader();
  }

  async onAdd(advImageModel: AdvImageModel){

    let isAdded = await this.advImageService.create(advImageModel);
    if(isAdded){
      this.images = await this.advImageService.getImages();

      this.toastService.show("تم اضافة الصورة بنجاح");
    }else{
      this.toastService.showErrorMsg();
    }

  }

  async onDelete(id: number){
    const alert = await this.alertController.create({
      header: 'حذف',
      message: 'هل تريد حذف الاعلان ؟',
      buttons: [
        {
          text: 'نعم',
          role: 'confirm',
          handler: async () => {
            this.loaderService.showLoader();
            
            let isDeleted = await this.advImageService.delete(id);
            if(isDeleted){
              this.images = await this.advImageService.getImages();

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

}
