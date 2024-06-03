using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    public Vehicle(
        Guid id,
        Model model,
        Vin vin ,
        Currency price ,
        Currency maintenance,
        DateTime lastDateRent,
        List<Accessory> accessories ,
        Address? address
    ): base(id)
    {
        Model = model;
        Vin = vin ;
        Price = price ;
        Maintenance = maintenance;
        LastDateRent = lastDateRent;
        Accessories = accessories ;
        Address = address;
    }
    public Vin? Vin { get; private set; }

    public Model? Model { get; private set; }

    public Address? Address { get; private set; }

    public Currency? Price { get; private set; }

    public Currency? Maintenance { get; private set; }

    public DateTime? LastDateRent { get; internal set; }

    public List<Accessory>? Accessories { get; private set;} = new();


} 



