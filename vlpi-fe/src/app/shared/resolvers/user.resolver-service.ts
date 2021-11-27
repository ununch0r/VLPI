import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from "../models/user.model";
import { UserSyncService } from '../services/user.sync.service';

@Injectable({providedIn: 'root'})
export class UserResolverService implements Resolve<User>{
    constructor(private userService: UserSyncService){}

    resolve(route : ActivatedRouteSnapshot, state : RouterStateSnapshot){
        let user = this.userService.currentUser;
        if(user === undefined)
        {
            return this.userService.setCurrentUser();
        }
        else{
            return user;
        }
    }
}