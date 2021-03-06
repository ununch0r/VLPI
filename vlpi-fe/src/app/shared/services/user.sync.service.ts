import { Injectable } from "@angular/core";
import { map, Observable, Subject } from "rxjs";
import { User } from "../models/user.model";
import { UserWebService } from "../web-services/user.web-service";

@Injectable()
export class UserSyncService {
    private usersSubj = new Subject<User[]>();
    private userSubj = new Subject<User>();
    userObs = this.userSubj.asObservable();
    usersObs = this.usersSubj.asObservable();
    isUserAuthorizedObs: Observable<boolean>;
    userNameObs: Observable<string>;
    currentUser: User;
    users: User[];

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

    reloadUsers()
    {
      this.userWebService.getUsers().subscribe(users => 
        {
          this.usersSubj.next(users)
          this.users = users;
        });
    }

    setCurrentUser(){
        var userObs = this.userWebService.getCurrentUser();
        userObs.subscribe(user => 
            {
                this.userSubj.next(user);
                this.currentUser = user;
            })
        return userObs;
    }

    setUser(user: User){
        this.userSubj.next(user)
    }

    isUserAdmin(): boolean{
        return this.currentUser.roles.findIndex(role => role ==='Admin') !== -1;
    }

    isUserAdminById(userId: number){
        let user = this.users.find(user => user.id === userId);
        return user.roles.findIndex(role => role ==='Admin') !== -1;
    }

    addAdminRole(userId: number){
        this.userWebService.addAdminRole(userId).subscribe(result => {
            this.reloadUsers();
        })
    }

    removeAdminRole(userId: number){
        this.userWebService.removeAdminRole(userId).subscribe(result => {
            this.reloadUsers();
        })
    }
}