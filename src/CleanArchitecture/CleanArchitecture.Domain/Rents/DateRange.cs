namespace CleanArchitecture.Domain.Rents;

public sealed record DateRange
{
    private DateRange()
    {

    }

    public DateOnly Start {get ; init;}

    public DateOnly End {get ; init;}

    public int NumberDays => End.DayNumber - Start.DayNumber ;

    public static DateRange Create (DateOnly start , DateOnly  end)
    {
        if(start > end)
        {
            throw new ApplicationException("End Date is less than start date");
        }

        return new DateRange
        {
            Start  = start , End = end
            
        };

    }



}