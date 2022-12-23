import { Injectable } from "@angular/core";
import { MessageService } from "primeng/api";

@Injectable()
export class ToastService {
    constructor(
        private messageService: MessageService) { }

    show(message: string) {
        this.messageService.add({severity:'success', summary: message});
    }

    showWarning(message: string) {
        this.messageService.add({severity:'warn', summary: message});
    }

    showErrorMsg() {
        this.messageService.add({severity:'error', summary: "حدث خطأ يرجي المحاولة لاحقا"});
    }
}