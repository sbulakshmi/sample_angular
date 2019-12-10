import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Photo } from '../models/vehicle';
import { map } from 'rxjs/operators';


@Injectable()
export class PhotoService {

    constructor(private http: HttpClient) { }

    public upload(vehicleId, file) {
        var formdata = new FormData();
        formdata.append("file", file);
        return this.http.post<any>(`/api/vehicles/${vehicleId}/photos`, formdata, {
            reportProgress: true,
            observe: 'events'
          }).pipe(map((event) => {
      
            switch (event.type) {
      
              case HttpEventType.UploadProgress:
                const progress = Math.round(100 * event.loaded / event.total);
                return { status: 'progress', percentage: progress };
      
              case HttpEventType.Response:
                return event.body;
              default:
                return `Unhandled event: ${event.type}`;
            }
          })
          );

    }
    private fileUploadProgress(event) {
        const percentDone = Math.round(100 * event.loaded / event.total);
        return { total: event.total, percentage: percentDone };
    }

    private apiResponse(event) {
        return event.body;
    }

    private handleError(error: HttpErrorResponse) {
        return error;
        // if (error.error instanceof ErrorEvent) {
        //     // A client-side or network error occurred. Handle it accordingly.
        //     console.error('An error occurred:', error.error.message);
        // } else {
        //     // The backend returned an unsuccessful response code.
        //     // The response body may contain clues as to what went wrong,
        //     console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);
        // }
        // // return an observable with a user-facing error message
        // return throwError('Something bad happened. Please try again later.');
    }
    private getEventMessage(event: HttpEvent<any>, formData) {

        switch (event.type) {

            case HttpEventType.UploadProgress:
                return this.fileUploadProgress(event);

            case HttpEventType.Response:
                return this.apiResponse(event);

            default:
                return `File "${formData.get('profile').name}" surprising upload event: ${event.type}.`;
        }
    }

    getPhotos(vehicleId) {
        return this.http.get<[Photo]>(`/api/vehicles/${vehicleId}/photos`);
    }
}