import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlServiceService {

  private baseUrl = 'http://localhost:3000';

  constructor() { };

  getUrls(page: number = 1, category: string = 'All') {
    return fetch(`${this.baseUrl}/api/urls.php?page=${page}&category=${category}`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include'
    });
  }

  loadCategories() {
    return fetch(`${this.baseUrl}/api/categories.php`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
      credentials: 'include'

    });
  }

  addUrl(url: string, description: string, category: string) {
    console.log(JSON.stringify({ url, description, category }))
    return fetch(`${this.baseUrl}/api/urls.php`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ url, description, category }),
      credentials: 'include'
    });
  }

  editUrl(id: number, url: string, description: string, category: string) {
    return fetch(`${this.baseUrl}/api/url.php?id=` + id, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ url, description, category }),
      credentials: 'include'
    });


  }

  deleteUrl(id: number) {
    return fetch(`${this.baseUrl}/api/url.php?id=` + id, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include'
    });
  }


}
