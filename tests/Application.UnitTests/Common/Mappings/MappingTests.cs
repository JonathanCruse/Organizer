using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
using Organizer.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Organizer.Application.TodoLists.Queries.GetTodos;
using Organizer.Domain.Entities;
using NUnit.Framework;
using Organizer.Application.FinancialService.Dtos;

namespace Organizer.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(TodoList), typeof(TodoListDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    [TestCase(typeof(TodoList), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(LookupDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemBriefDto))]
    [TestCase(typeof(Collective), typeof(CollectiveDto))]
    [TestCase(typeof(Feminist), typeof(FeministDto))]
    [TestCase(typeof(FeministCollective), typeof(FeministCollectiveDto))]
    [TestCase(typeof(Transaction), typeof(TransactionDto))]
    [TestCase(typeof(Expense), typeof(ExpenseDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
