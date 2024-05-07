

namespace Catalog.Api.Exceptions;

public class ProductNotFountException(Guid id) : NotFountException("Product not found",id);