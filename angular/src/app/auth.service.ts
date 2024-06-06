import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:3000';

  constructor() { }

  login(username: string, password: string) {
    console.log(JSON.stringify({ username, password }));
    return fetch(`${this.baseUrl}/login.php `, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ username, password }),
      credentials: 'include'
    });
  }

  register(username : string, password :string){
    return fetch(`${this.baseUrl}/register.php`,{
      method: 'POST',
      headers:{
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({username,password}),
      credentials: 'include'
    })
  }
}
