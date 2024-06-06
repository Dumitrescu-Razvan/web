import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username : string = '';
  password : string = '';

  constructor(private authService : AuthService,private router : Router) {};

  login(event : Event){
    event.preventDefault();
    this.authService.login(this.username, this.password).then((response) => {
      //response = [success : true/false]
      response.status === 200 ? this.router.navigate(['/urls']) : alert('Login failed');
      
    });
    
  }

}
