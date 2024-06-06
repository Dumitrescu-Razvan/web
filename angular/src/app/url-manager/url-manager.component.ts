import { Component } from '@angular/core';
import { UrlServiceService } from '../url-service.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-url-manager',
  standalone: true,
  imports: [NgFor, NgIf,FormsModule],
  templateUrl: './url-manager.component.html',
  styleUrl: './url-manager.component.css'
})
export class UrlManagerComponent {
  urls: any[] = [];
  next: boolean = false;
  prev: boolean = false;
  categories: any[] = [];
  currentPage = 1;
  currentCategory : string = 'All';
  editingUrl : any = null;

  constructor(private urlService: UrlServiceService) {};

  ngOnInit(){
    this.loadUrls();
    this.loadCategories();
  }

loadUrls(page = 1, category = 'All'){
  this.currentPage = page;
  console.log(this.currentPage, this.currentCategory);
  this.urlService.getUrls(page, this.currentCategory).then(response => {
    response.json().then(data => {
      this.urls = data.urls;
      this.next = data.has_next;
      this.prev = data.has_prev;
    });
  });
}

  loadCategories(){
    this.urlService.loadCategories().then(response => {
      response.json().then(data => {
        for (let category of data){
          if (!this.categories.includes(category.category)) {
            this.categories.push(category.category);
          }
        }
      });
    });
  }

  addUrl(event: Event)
  {
    event.preventDefault();
    const target = event.target as any;
    const url = target.querySelector('#url').value;
    const description = target.querySelector('#description').value;
    const category = target.querySelector('#category').value;
    this.urlService.addUrl(url, description, category).then(response => {
      response.json().then(data => {
        this.loadUrls();
      });
    });
  }

  editUrl(event: Event,url: any){
    event.preventDefault();
    this.editingUrl = url;
  } 

  submitEdit(event: Event, url: any){
    event.preventDefault();
    const target = event.target as any;
    const newurl = target.querySelector('#newurl').value;
    const newdescription = target.querySelector('#newdescription').value;
    const newcategory = target.querySelector('#newcategory').value;
    console.log(newurl,newdescription,newcategory,url.id)
    this.urlService.editUrl(url.id,newurl,newdescription,newcategory).then(response => {
      response.json().then(data => {
        this.loadUrls();
        this.editingUrl = null;
      });
    }
    );
  }

  deleteUrl(id: number){
    this.urlService.deleteUrl(id).then(response => {
      response.json().then(data => {
        this.loadUrls();
      });
    });
  }

  filterUrls(event: Event){
    const target = event.target as any;
    const category = target.querySelector('#category').value;
    this.loadUrls(1, this.currentCategory);
  }
}
