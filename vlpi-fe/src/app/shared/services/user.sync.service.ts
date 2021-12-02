import { Injectable } from "@angular/core";
import { map, Observable, Subject } from "rxjs";
import { User } from "../models/user.model";
import { UserWebService } from "../web-services/user.web-service";

@Injectable()
export class UserSyncService {
    private userSubj = new Subject<User>();
    userObs = this.userSubj.asObservable();
    isUserAuthorizedObs: Observable<boolean>;
    userNameObs: Observable<string>;
    currentUser: User;

    constructor(private userWebService: UserWebService){
        this.userNameObs = this.userObs.pipe(map(user => {
            if(!!user){
                return user.firstName
            }
            else{
                return '';
            }
        }));
        this.isUserAuthorizedObs = this.userObs.pipe(map(user => !!user));
    }

    setCurrentUser(){
        this.userWebService.getCurrentUser().subscribe(user => 
            {
                this.userSubj.next(user);
                this.currentUser = user;
            })
        return this.currentUser;
    }

    setUser(user: User){
        this.userSubj.next(user)
    }

    isUserAdmin(): boolean{
        return this.currentUser.roles.findIndex(role => role ==='Admin') !== -1;
    }
}