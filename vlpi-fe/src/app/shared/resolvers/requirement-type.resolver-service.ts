import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { TaskSyncService } from 'src/app/requirements/services/task.sync-service';
import { RequirementType } from '../models/requirement-type.model';

@Injectable({providedIn: 'root'})
export class RequirementTypeResolverService implements Resolve<RequirementType[]>{
    constructor(private taskService: TaskSyncService){}

    resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot){
        let types = this.taskService.requirementTypes;
        if(types === undefined)
        {
            return this.taskService.reloadReqruirementTypes()
        }
        else{
            return types;
        }
    }
}