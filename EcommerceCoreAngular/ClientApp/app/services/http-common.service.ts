import { Injectable } from "@angular/core";
import { Http, Response, ResponseContentType, Headers, RequestOptions, RequestOptionsArgs, Request, RequestMethod, URLSearchParams } from "@angular/http";
//import { Observable } from 'rxjs/Observable';
//import { Observable } from "rxjs/Rx";
import { Observable } from 'rxjs/Rx'



@Injectable()
export class HttpCommonService {
    public apiBaseUrl: string;

    constructor(public http: Http) {
        this.http = http;
        this.apiBaseUrl = "/";
    }

    PostRequest(apiName: string, model: any) {

        let headers = new Headers();
        headers.append("Content-Type", 'application/json');
        let requestOptions = new RequestOptions({
            method: RequestMethod.Post,
            url: this.apiBaseUrl + apiName,
            headers: headers,
            body: JSON.stringify(model)
        })

        return this.http.request(new Request(requestOptions))
            .map((res: Response) => {
                if (res) {
                    return [{ status: res.status, json: res.json() }]
                }
            });
    }
    requestOptions() {
        let contentType = 'application/json';//"x-www-form-urlencoded";
        let headers = new Headers({ 'Content-Type': contentType });
        let options = new RequestOptions({
            headers: headers,
            //body: body,
            // url: this.apiBaseUrl + apiName,
            // method: requestMethod,
            //responseType: ResponseContentType.Json
        });
        return options;
    }
    stringifyModel(model: any) {
        return JSON.stringify(model);
    }

    create(apiName: string, model: any) {

        // let headers = new Headers({ 'Content-Type': 'application/json' });
        // let options = new RequestOptions({ headers: headers });
        // let body = JSON.stringify(model);
        return this.http.post(this.apiBaseUrl + apiName,
            //  this.stringifyModel(model),
            model//, this.requestOptions()
        )
            .map(this.extractData)  //.map((res: Response) => res.json()) 
            .catch(this.handleError)
            // .subscribe()
            ;

    }
    update(apiName: string, model: any) {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        // let options = new RequestOptions({ headers: headers });
        //let body = JSON.stringify(model);
        return this.http.put(this.apiBaseUrl + apiName, model,
            this.requestOptions()).map(
                (res: Response) => res.json()
            ).catch(this.handleError);//.subscribe();
    }
    delete(apiName: string, id: number) {
        return this.http.delete(this.apiBaseUrl + apiName + '?id=' + id)
            .catch(this.handleError);//.subscribe();;
    }

    getList(apiName: string) {
        return this.http.get(this.apiBaseUrl + apiName, { search: null })
            .map((res: Response) => {
                console.log(res);
                return res.json();
            }).catch(this.handleError);
        /*let myHeaders = new Headers();
            myHeaders.append('Content-Type', 'application/json');
            let myParams = new URLSearchParams();
            myParams.append('id', '1');
            let options = new RequestOptions({ headers: myHeaders, params: myParams });
            return this.http.get(this.apiBaseUrl + apiName, options)
                .map((responseData) => responseData.json()).catch(this.handleError);*/
        /*this.http.get(this.apiBaseUrl + apiName).subscribe(result => {
            console.log(result);
        }, error => console.error(error));*/
    }
    getItem(apiName: string, id: number) {

        return this.http.get(this.apiBaseUrl + apiName + "?id=" + id, { search: null })
            .map((responseData) => responseData.json()).catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || {};
    }

    private handleError(error: Response | any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);

        return Observable.throw(errMsg);
    }
}