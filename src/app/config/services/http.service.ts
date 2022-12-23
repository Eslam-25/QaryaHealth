import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";


@Injectable()
export class HttpService{
    
    baseUrl: string = environment.apiURL;
    constructor(private httpClient: HttpClient){}

    get<T>(segment: string): Observable<T>{
        return this.httpClient.get(this.baseUrl + segment) as Observable<T>;
    }

    getById<T>(segment: string, id: number): Observable<T>{
        return this.httpClient.get(this.baseUrl + segment + '/' + id) as Observable<T>;
    }

    getByValue<T>(segment: string, value: string): Observable<T>{
        return this.httpClient.get(this.baseUrl + segment + '/' + value) as Observable<T>;
    }

    getByQueryParam<T>(segment: string, queryParam: string): Observable<T>{
        return this.httpClient.get(this.baseUrl + segment + '/' + queryParam) as Observable<T>;
    }

    post(segment: string, body: any): Observable<boolean>{
        return this.httpClient.post(this.baseUrl + segment, body) as Observable<boolean>;
    }

    postWithType<T>(segment: string, body: any): Observable<T>{
        return this.httpClient.post(this.baseUrl + segment, body) as Observable<T>;
    }

    postWithJsonReponse(segment: string, body: any){
        return this.httpClient.post(this.baseUrl + segment, body, {responseType: "json"});
    }

    put(segment: string, body: any): Observable<boolean>{
        return this.httpClient.put(this.baseUrl + segment, body) as Observable<boolean>;
    }

    delete(segment: string, body: any): Observable<boolean>{
        let options = {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            }),
            body: body,
          };
        return this.httpClient.delete(this.baseUrl + segment, options ) as Observable<boolean>;
    }
}