import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { ExecutionModeSyncService } from 'src/app/requirements/services/execution-mode.sycn-service';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { TaskWebService } from 'src/app/requirements/web-services/task.web-service';
import { UtilsWebService } from 'src/app/requirements/web-services/utils.web-service';
import { RequirementType } from '../models/requirement-type.model';

@Injectable({providedIn: 'root'})
export class RequirementTypeResolverService implements Resolve<RequirementType[]>{
    constructor(private taskService: TaskSyncService, private utilsService: UtilsWebService){}

    resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot){
        let types = this.taskService.requirementTypes;
        if(types === undefined)
        {
            return this.utilsService.getRequirementTypes();
        }
        else{
            return types;
        }
    }
}