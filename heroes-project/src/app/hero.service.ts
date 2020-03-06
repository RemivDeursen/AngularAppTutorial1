import { HEROES } from './mock-heroes';
import { Injectable } from '@angular/core';
import { Hero } from './hero';
import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private heroesUrl = 'api/heroes';

  getHeroes(): Observable<Hero[]> {
    this.log('Heroes retrieved');
    return this.http.get<Hero[]>(this.heroesUrl);
    // return of(HEROES);
  }

  getHero(id: number): Observable<Hero> {
    this.log('Hero details retrieved');
    return of(HEROES.find(hero => hero.id === id));
  }
  constructor(private messageService: MessageService,
    private http: HttpClient) { }

  private log(message: string) {
    this.messageService.add(`HeroService: ${message}`);
  }
}
