import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ModalController } from '@ionic/angular';
import { LoaderService } from '../../../../config/services/loader.service';
import { ToastService } from '../../../../config/services/toast.service';
import { AuthService } from '../../../auth/services/auth.service';
import { MedicalStaffServiceModel } from '../../models/medical-staff-service.model';
import { MedicalStaffServiceTypeService } from '../../services/medical-staff-service-type.service';

@Component({
  selector: 'app-staff-service-form',
  templateUrl: './staff-service-form.component.html',
  styleUrls: ['./staff-service-form.component.css']
})
export class StaffServiceFormComponent implements OnInit {

  submitted = false;
  staffService: MedicalStaffServiceModel;
  serviceForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private modalCtrl: ModalController,
    private toastService: ToastService,
    private loaderService: LoaderService,
    private staffServiceType: MedicalStaffServiceTypeService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.prepareFormGroup()
  }

  prepareFormGroup() {
    this.serviceForm = this.fb.group({
      name: [this.staffService?.name, Validators.required],
      description: [this.staffService?.description],
      cost: [this.staffService?.cost, Validators.required],
    });
  }

  get verfiyFormControls() {
    return this.serviceForm.controls;
  }

  async mapModelFromUI() {
    if(this.staffService == null) 
      this.staffService = {} as MedicalStaffServiceModel;

    this.staffService.name = this.serviceForm.get("name")?.value;
    this.staffService.description = this.serviceForm.get("description")?.value;
    this.staffService.cost = this.serviceForm.get("cost")?.value;
    this.staffService.staffId = await this.authService.userId();
    this.staffService.isActive = true;
  }

  async onAdd(){
    let isAdded = await this.staffServiceType.create(this.staffService);
    if(isAdded){
      this.toastService.show("تمت الاضافة بنجاح");
      return this.modalCtrl.dismiss({isSuccessfully: true});

    }else{
      this.toastService.showErrorMsg();
    }
  }

  async onUpdate(){
    let isUpdated = await this.staffServiceType.update(this.staffService);
    if(isUpdated){
      this.toastService.show("تم التعديل بنجاح");
      return this.modalCtrl.dismiss({isSuccessfully: true});

    }else{
      this.toastService.showErrorMsg();
    }
  }

  async onSave() {
    this.submitted = true;
    if (this.serviceForm.valid) {
      await this.mapModelFromUI();

      this.loaderService.showLoader();
      if(this.staffService?.id > 0){
        await this.onUpdate();
      }else{
        await this.onAdd();
      }

      this.loaderService.hideLoader();
    }
  }

  onCancel(){
    return this.modalCtrl.dismiss();
  }
}
