import { Injectable } from "@angular/core";
import { BloodType } from "../../../config/enums/blood-types.enum";
import { HttpService } from "../../../config/services/http.service";
import { DonarModel } from "../models/donar.model";

@Injectable()
export class DonorService{
    segment: string = "Donor";

    constructor(private httpService: HttpService) { }

    getDonar(id: number) {
        return this.httpService.get<DonarModel>(`${this.segment}/${id}`).toPromise<DonarModel>();
    }

    toggleDonoration(id: number) {
        return this.httpService.get<boolean>(`${this.segment}/ToggleDonor/${id}`).toPromise<boolean>();
    }

    getDonars(type: BloodType) {
        return this.httpService.get<DonarModel[]>(`${this.segment}/ByBloodType/${type}`).toPromise<DonarModel[]>();
    }

    update(donor: DonarModel) {
        return this.httpService.put(`${this.segment}`, donor).toPromise<boolean>();
    }
}