# historianProductionService

historian addition of product in op

## ProductHistorian

product annotation used in production

* type: indicates product used input or output.
  * enum
  * values input or output
  * Required
* productionOrderId: id production order
  * integer
  * Required
* productId: id of product
  * integer
  * Required
* quantity: quantity of used
  * double
  * Required
* batch: batch of product
  * String (50 characters)
  * Required
* username: user loged into the system
  * String 

### JSON Example

```json
{
  "type": "output",
  "productionOrderId": 1,
  "productId": 6,
  "quantity": 2.5,
  "batch": "lote",
  "username": "fernando"
}
```

## Url ProductHistorian

* /api/ProductHistorian
  * POST: Create productHistorian

## OrderHistorian

product annotation used in production

* productionOrderId: id of production order.
  * integer
* order: order number
  * integer
* productsInput: list of product used input
  * list object (Product)
* productsOutput: list of product used output

  * list object (Product)

* Object (Product):
* productId: id of product
  * integer
* product: name of product
  * String
* quantity: quantity used
  * double
* batch: batch used
  * String
* date: date used in ticks
  * long

### JSON Example

```json
{
  "id": 1,
  "productionOrderId": 1,
  "order": "2121",
  "productsInput": [
    {
      "id": 1,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636500730924588763
    },
    {
      "id": 2,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636500732926469057
    },
    {
      "id": 3,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636500749113058432
    },
    {
      "id": 6,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636501521152354715
    }
  ],
  "productsOutput": [
    {
      "id": 4,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636501437805290124
    },
    {
      "id": 5,
      "productId": 5,
      "product": "deby",
      "quantity": 2.5,
      "batch": "lote",
      "date": 636501520844958892
    }
  ]
}
```

## Url OrderHistorian

* /api/OrderHistorian/{id}
  * GET: Return historian order

# GatewayAPI

API Responsible to provide access to information nedeed to compose the production order from other APIs

## URLs

* gateway/productionorders/{optional=startat}{optional=quantity}{optional=orderField}{optional=order}{optional=fieldFilter}{optional=fieldValue}

  * Get: Return List of Production Order
    * startat: represent where the list starts t the database (Default=0)
    * quantity: number of resuls in the query (Default=50)
    * orderField: Field in which the list will be order by (Possible Values:
      productionOrderNumber,currentstatus)
      productGTIN)(Default=ProductId)
    * order: Represent the order of the listing (Possible Values: ascending,
      descending)(Default=Ascending)
    * fieldFilter: represents the field that will be seached (Possible Values:
      productionOrderNumber,currentstatus) (Default=null)
    * fieldValue: represents de valued searched on the field (Default=null)
  * Post: Create the Production Order with the JSON in the body
    * Body: Production Order JSON

* gateway/productionorders/{id}

  * Get: Return Production Order with productionOrderId = ID
