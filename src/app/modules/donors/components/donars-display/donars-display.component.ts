import { Component, OnInit } from '@angular/core';
import { DonarModel } from '../../models/donar.model';
import { Gender } from "../../../../config/enums/gender.enum";
import { BloodType } from '../../../../config/enums/blood-types.enum';
import { AlertController } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { LoaderService } from '../../../../config/services/loader.service';
import { DonorService } from '../../services/donor.service';

@Component({
  selector: 'app-donars-display',
  templateUrl: './donars-display.component.html',
  styleUrls: ['./donars-display.component.css']
})
export class DonarsDisplayComponent implements OnInit {

  isLoaded = false;
  donars: DonarModel[] = [];
  bloodType: BloodType = BloodType.APlus;

  constructor(
    private alertController: AlertController,
    private activatedRoute: ActivatedRoute,
    private loaderService: LoaderService,
    private donarService: DonorService
    ) { }

  async ngOnInit() {
    this.loaderService.showLoader();
    this.activatedRoute.queryParams.subscribe(params => this.bloodType = params.type);
    await this.prepareDonars();
  }
  
  async prepareDonars() {
    this.donars = await this.donarService.getDonars(this.bloodType);
    this.isLoaded = true;
    this.loaderService.hideLoader();
  }

  async openFilter() {
    const alert = await this.alertController.create({
      subHeader:"بحث بالاسم / العنوان",
      buttons: [
        {
          text: 'بحث',
          cssClass: 'alert-button-confirm',
          handler: async (alertData) => {
            if (!alertData.searchValue) {
              return false;
            } else {
              
              //make HTTP call
              console.log(alertData.searchValue);
            }
          }
        }
      ],
      inputs: [
        {
          placeholder: 'الاسم / العنوان',
          name: "searchValue",
          attributes: {
            minLength: 1,
          }
        }
      ],
    });

    await alert.present();
  }

}
