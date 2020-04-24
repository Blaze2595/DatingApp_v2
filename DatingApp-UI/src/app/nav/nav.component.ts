import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged in Successful');
    }, error => {
      console.log('Log-in Failed');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token; // If token empty it'll return false else true
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Logged Out');
  }

}
