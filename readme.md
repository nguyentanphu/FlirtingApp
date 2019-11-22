# Initial release of clean architecture sample app

<img src="https://serving.photos.photobox.com/3047955071e2f47a1e3c1140fb2f279d5472e04913a6d9fa7b7ed858bcf5ed415020f202.jpg"
     alt="Clean architecture" />

## Feature

- ASP.NET Core 3, ASP.NET Core 3 Identity, angular 8
- Jwt Authentication with refresh token.
- Simple, focus on intention of the code.
- Single responsibility classes.
- Separation of concerns.
- CQRS with Mediator.
- Validation with FluentValidation.
- Remove usage of default ASP.NET Core validation with ModelState and move validation to Application layer as it's part of business logic.
- Cloudinary integration for uploading images
- Custom exceptions for each layers.
- Unify exceptions errors message with ExceptionMiddleware
- 2 DbContext: 1 for Application entities, 1 for IdentityDbContext.

## In progress

- Update tests for Application layer.
- Dockerizing the app.
