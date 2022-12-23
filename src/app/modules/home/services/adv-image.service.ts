import { Injectable } from "@angular/core";
import { HttpService } from "../../../config/services/http.service";
import { AdvImageModel } from "../models/adv-image.model";

@Injectable()
export class AdvImageService{
    segment: string = "AdvImage";

    constructor(private httpService: HttpService) { }

    create(advImage: AdvImageModel) {
        return this.httpService.post(`${this.segment}`, advImage).toPromise<boolean>();
    }

    getImages() {
        return this.httpService.get<AdvImageModel[]>(`${this.segment}`).toPromise<AdvImageModel[]>();
    }

    delete(id: number) {
        return this.httpService.delete(`${this.segment}/${id}`, id).toPromise<boolean>();
    }

    uploadImage(image: any){
        return this.httpService.postWithType<any>("UploadImage", image).toPromise<any>();
    }
}