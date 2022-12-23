import { Injectable } from "@angular/core";
import { MedicalStaffType } from "../../../config/enums/medical-staff-types.enum";
import { HttpService } from "../../../config/services/http.service";
import { MedicalStaffModel } from "../models/medical-staff.model";

@Injectable()
export class MedicalStaffService{
    segment: string = "MedicalStaff";

    constructor(private httpService: HttpService) { }

    getStaff(id: number) {
        return this.httpService.get<MedicalStaffModel>(`${this.segment}/${id}`).toPromise<MedicalStaffModel>();
    }

    getStaffes(type: MedicalStaffType) {
        return this.httpService.get<MedicalStaffModel[]>(`${this.segment}/ByType/${type}`).toPromise<MedicalStaffModel[]>();
    }

    ToggleWorking(id: number) {
        return this.httpService.get<boolean>(`${this.segment}/ToggleWorking/${id}`).toPromise<boolean>();
    }

    create(staff: MedicalStaffModel) {
        return this.httpService.post(`${this.segment}`, staff).toPromise<boolean>();
    }

    update(staff: MedicalStaffModel) {
        return this.httpService.put(`${this.segment}`, staff).toPromise<boolean>();
    }

    uploadImage(image: any){
        return this.httpService.postWithType<any>("UploadImage", image).toPromise<any>();
    }
}