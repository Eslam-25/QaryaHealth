import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class LoaderService{

    loader = new BehaviorSubject<boolean>(false);

    showLoader(){
        this.loader.next(true);
    }

    hideLoader(){
        this.loader.next(false);
    }
}