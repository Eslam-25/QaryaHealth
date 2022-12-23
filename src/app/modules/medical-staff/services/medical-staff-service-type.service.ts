import { Injectable } from "@angular/core";
import { HttpService } from "../../../config/services/http.service";
import { MedicalStaffServiceModel } from "../models/medical-staff-service.model";

@Injectable()
export class MedicalStaffServiceTypeService{
    segment: string = "MedicalStaffService";

    constructor(private httpService: HttpService) { }

    create(service: MedicalStaffServiceModel) {
        return this.httpService.post(`${this.segment}`, service).toPromise<boolean>();
    }

    update(service: MedicalStaffServiceModel) {
        return this.httpService.put(`${this.segment}`, service).toPromise<boolean>();
    }

    delete(id: number) {
        return this.httpService.delete(`${this.segment}/${id}`, id).toPromise<boolean>();
    }
}