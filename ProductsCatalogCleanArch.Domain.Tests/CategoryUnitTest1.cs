using FluentAssertions;
using ProductsCatalogCleanArch.Domain.Entities;
using ProductsCatalogCleanArch.Domain.Validation;
using System;
using Xunit;

namespace ProductsCatalogCleanArch.Domain.Tests
{
    public class CategoryUnitTest1
    {
        //DisplayName renomeia o teste
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            //criando uma instancia da classe Category
            //esperasse que o teste não lance uma exceção no dominio
            Action action = () => new Category(1, "Category Name ");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name ");
            action.Should()
                .Throw<DomainExceptionValidation>()//espera-se que lance uma exceção
                 .WithMessage("Invalid Id value.");//teste valida se a mensagem de erro está como foi definida na classe
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "Ca");//testa regra de negócio (não pode ser menor que 3 caracteres)
            action.Should()
                .Throw<DomainExceptionValidation>()
                   .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact]
        public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name.Name is required");
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}
