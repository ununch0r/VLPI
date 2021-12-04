import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { ExecutionModeSyncService } from 'src/app/requirements/services/execution-mode.sycn-service';
import { UtilsWebService } from 'src/app/requirements/web-services/utils.web-service';
import { ExecutionMode } from "../models/execution-mode.model";

@Injectable({providedIn: 'root'})
export class ExecutionModeResolverService implements Resolve<ExecutionMode[]>{
    constructor(private executionModeService: ExecutionModeSyncService, private utilsService: UtilsWebService){}

    resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot){
        let executionModes = this.executionModeService.executionModes;
        if(executionModes === undefined)
        {
            return this.utilsService.getExecutionModes();
        }
        else{
            return executionModes;
        }
    }
}