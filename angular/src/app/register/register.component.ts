import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  username : string = '';
  password : string = '';

  constructor(private authService : AuthService, private router : Router) {};

  register(event : Event){
    event.preventDefault();
    this.authService.register(this.username, this.password).then((respose) =>{
    respose.status === 200 ? this.router.navigate(['/login']) : alert('Registration failed');
  });
}


}
