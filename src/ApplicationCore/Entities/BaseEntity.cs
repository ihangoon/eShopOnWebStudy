using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    // This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    // Using non-generic integer types for simplicity and to ease caching logic
    // 이 클래스는 다양한 키 타입을 지원하기 위해 BaseEntity<T>와 public T Id로 바뀌기 쉽다.
    // 단순함과 캐싱 로직을 쉽게 하기 위해 제네릭 타입이 아닌 integer 타입을 사용한다.
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }
    }
}
