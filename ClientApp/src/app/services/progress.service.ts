import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { BrowserXhr } from '@angular/http';
//import { BrowserXhr } from '@angular/common/http/src/xhr';

@Injectable()
export class ProgressService {

    private uploadProgress: Subject<any>;// = new Subject();
    startTracking()
    {
        console.log("1");
        this.uploadProgress =  new Subject();
        console.log(this.uploadProgress);
        return this.uploadProgress;
    }
    notifyProgress(progress)
    {
        console.log(progress);
        this.uploadProgress.next(progress);
    }

    endTracking()
    {
        this.uploadProgress.complete();
        console.log(this.uploadProgress);
    }
    downloadProgress: Subject<any> = new Subject();
}


@Injectable()
export class BrowserXhrWithProgress extends BrowserXhr {
    private startTime;;
    constructor(private service: ProgressService) {   super();  console.log("3"); }
    build(): XMLHttpRequest {
        console.log("2");
         this.startTime = Date.now();
        var xhr: XMLHttpRequest = super.build();
       
        xhr.onprogress = (event) => {
            this.service.downloadProgress.next(this.createProgress(event));
        }

        xhr.upload.onprogress = (event) => {
            this.service.notifyProgress(this.createProgress(event));
        }

        xhr.upload.onloadend = () => {
          
            this.service.endTracking();//
           
        }


        return xhr;
    }
    private createProgress(event) {

        const timeElapsed = Date.now() - this.startTime;
        const uploadSpeed = event.loaded / (timeElapsed / 1000);
         const timeRemaining = Math.ceil(((event.total - event.loaded) / uploadSpeed));

        event.uploadTimeElapsed = Math.ceil(timeElapsed / 1000);
        event.uploadTimeRemaining = timeRemaining;
        event.uploadSpeed = (uploadSpeed / 1024 / 1024).toFixed(2);
        event.percentCompleted = ((event.loaded / event.total) * 100).toFixed(0);
        //event.uploadId = uploadId;
        console.log(event);
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        }
    }
}