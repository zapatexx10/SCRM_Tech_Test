# How to execute the docker file
Execute this command inside the project root folder, where the Dockerfile is located:
```bash
docker build -t promotion-engine-api .
```
Then, to run the container:
```bash
docker build --build-arg BUILD_CONFIGURATION=Debug -t promotion-engine-api .

docker run -d -p 8080:80 --name promotion-engine-api-container promotion-engine-api
```
I set it to debug mode in case that we want to debug the code, but in production we should set it to release mode.

To stop the container:
```bash
docker stop promotion-engine-api-container
```

# Explanations and decisions I took

# Backend
## Renaming and refactors
Some files like the Handlers, Requests, Responses and the repository were renamed to be more specific and clear. For example, the handler was renamed to GetPromotionsHandler, the request to GetPromotionsRequest and the response to GetPromotionsResponse. This way, it is more clear what each file is doing and it is easier to find them in the future.

Followed the Vertical Slice Architecture, by features (GetById, GetAll) inside the Promotions Featrues.

I moved the PromotionModel to the SharedFolder due that we use it in two different eatures.

## Mapping the Entity to the Model
I created an extension method to map the PromotionEntity to the PromotionModel, this way we can keep the mapping logic in one place and it is easier to maintain and test.
I wanted to avoid using AutoMapper or other libraries, since it is a library that adds an extra layer of complexity and it is not necessary for this simple mapping. By using an extension method, we can keep the code simple and easy to understand.

Also, whith this decision, I dont want to depend on a library that can change their business model and go by licence like Automapper, MassTransit and MediatR

## Created an interface for the PromotionRepository
I created an interface for the PromotionRepository, this way we can easily mock it in the unit tests and it is easier to maintain and test. Also, it is a good practice to depend on abstractions rather than concretions.
With this decision we use the Dependency Inversion Principle, which is one of the SOLID principles, and it helps us to decouple the code and make it more maintainable.

I moved the connection string and the initialization of the "database"

## PromotionRepository
First I created a GetAll returning a list of promotions, but investigating I prefer to return a IAsyncEnumerable, because it's more efficient, dont allocate the X number of Promotions in RAM memory...

## DatabaseConnectionFactory
I created a DatabaseConnectionFactory in order to set the connection string only once, and every Repository (if we had more) will benefit for this. Also we have the connection string only in one place instead of initialize and have it hardcoded in every Handler or Repository like it was before. 
Also, I did it because it can be useful in the future if we need to create multiple connections or if we need to add some logic before creating the connection.
And its easy to replace for a real database connection instead of a simulated one with the list(Postgresql, Mongo, SqlServer ...)


## Validations
I created two validation attributes using the library DataAnnotations
This two attributes checks that the language or country are not null or empty/whitespace, has two chars and that those chars are not digits.
I use them in the GetAll, and GetById endpoints. 

## HTTP RESTful
I see that the endpoints are not returning the http codes that it should and if we dont find something in the list it returns a 500 with the message "No element found", or something like this. I would loved to update the Request and make it generic, and add more types like Success, NotFound, Error...
But I realized at the end, that it would be a big change and it would affect a lot of code and tests that I would have to refactor, so I decided to leave it as it is. But I want to show you how I would do it if I had more time: 

```csharp

public abstract record Result<T>
{
    public record Success(T Value) : Result<T>;
    public record NotFound(string Message) : Result<T>;
    public record Error(string Message) : Result<T>;
}

// Examples of using:
if (promotion is null)
{
//Not found(404)
    return new Result<PromotionModel>.NotFound($"Discount {request.DiscountId} not found");
}

//Found(OK)
return new Result<PromotionModel>.Success(promotionModel);

// In the Controller:
return result switch
{
    Result<PromotionModel>.Success(var dto) => Ok(dto),
    Result<PromotionModel>.NotFound(var msg) => NotFound(new { error = msg }),
    Result<PromotionModel>.Error(var msg) => BadRequest(new { error = msg }),
    _ => StatusCode(500)
};
```
Or add an ExceptionHandlerMiddleWhare that checks the type of exception and returns the correct http code...


## Added the CORS 
I added the CORS policy in order to communicate propertly with the frontend.

# Tests
I create three different types of tests. But mainly we covered the happy path and the corner cases that will return controlled errors and data input validations in the controller. 

For the GetAll v1 and v2 I created the ITests for checking that the http endpoint works correctly from start to end, validate the parameters that we send to the endpoint etc etc. 

For the GetPromotionById I only created the unit tests due that the ITests are almost the same. 

The handler unit tests checks that the business logic works. 
The controller unit tests checks that the call of the Handler is correct, returns the expected httpCode, we expect that the mapping is correct, handles exceptions as expected...


# Frontend

## Redirect to the PromotionDetail

I tried to do the Route by file but I wasnt able to create the +type in order to use it in the Route so I set the type to any...

```javascript
import PromotionDetail from "../promotion-detail/PromotionDetail";
// import type { Route } from "./+types/Promotion.$promotionId";

export function meta({ params }: any) {
  return [
    { title: `Promotion ${params.promotionId}` },
    { name: "description", content: "Promotion details" },
  ];
}

export default function PromotionDetailRoute() {
  return <PromotionDetail />;
}
```
## Use of Link instead of onClick with navigate
I used the Link component instead of onClick because I was having some trouble with the navigate. 
And I also sent the country and language as query parameters in the url, so we can use them in the PromotionDetail component to fetch the promotion details with the correct country and language.

```javascript
{promotionList.map((promotion) => (
                                    // <article
      <Link
          key={promotion.promotionId}
          // to={`/promotion/${promotion.promotionId}`}
          to={`/promotion/${promotion.promotionId}?country=${selectedCountry}&lang=${selectedLanguage}`}
          className="flex flex-col sm:flex-row border sm:h-48 border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow duration-200 bg-white"
          aria-label={`Promotion ${promotion.texts.title}`}>
          {/* Image Side */}
          //More code...                                   
      </Link>
  ))}
```
#




