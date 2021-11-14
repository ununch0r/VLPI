import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable()
export class PageNameSyncService {
    private pageNameSubj = new Subject<string>();
    pageNameObs = this.pageNameSubj.asObservable();

    setPageName(pageName: string){
        this.pageNameSubj.next(pageName);
    }
}