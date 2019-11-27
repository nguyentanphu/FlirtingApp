import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({
      url: this.prepareUrl(req.url)
    });

    return next.handle(req);
  }

  private isAbsoluteUrl(url: string): boolean {
    const absolutePattern = /^(https|http)?:\/\//i;
    return absolutePattern.test(url);
}

  private prepareUrl(url: string): string {
    if (this.isAbsoluteUrl(url)) {
      return url;
    }

    return environment.apiUrl + url;
  }
}
