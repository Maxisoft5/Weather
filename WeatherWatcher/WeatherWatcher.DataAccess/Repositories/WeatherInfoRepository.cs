using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Linq.Expressions;
using WeatherWatcher.DataAccess.EFCore;
using WeatherWatcher.Domain.Entities;
using WeatherWatcher.DTO.EntityDTO;
using WeatherWatcher.DTO.Filters;
using WeatherWatcher.Services.Infrastructure.Repositories;

namespace WeatherWatcher.DataAccess.Repositories
{
    public class WeatherInfoRepository : IWeatherInfoRepository
    {
        private readonly DataContext _dataContext;
        public WeatherInfoRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<WeatherInfo>> GetPaginated(int offset, int size, 
            IEnumerable<FilterDTO<string>> stringFilters = null, 
            IEnumerable<FilterDTO<decimal>> decimalFilters = null, 
            IEnumerable<FilterDTO<int>> intFilters = null,
            IEnumerable<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null, 
            IEnumerable<FilterDTO<TimeOnly>> timeFilters = null)
        {
            IQueryable<WeatherInfo> query = _dataContext.WeatherInfos.AsQueryable();

            
            var expression = Expression.Parameter(query.ElementType, "e");
            Expression filterDateExpr = null;
            Expression filterTimeExpr = null;
            Expression filterExpr = null;
            if (dateOnlyFilters != null)
            {
                foreach (var dateOnlyFilter in dateOnlyFilters) 
                {
                    filterDateExpr = GetRangeDateOnlyExpressionForField(dateOnlyFilter, expression);
                }
                if (filterDateExpr != null)
                {
                    filterExpr = filterDateExpr;
                }
            }
            if (timeFilters != null)
            {
                foreach(var timeFilter in timeFilters)
                {
                    filterTimeExpr = GetTimeOnlyExpressionForField(timeFilter, expression);
                }
                if (filterExpr != null && filterTimeExpr != null)
                {
                    filterExpr = Expression.And(filterExpr, filterTimeExpr);
                } 
                else
                {
                    if (filterTimeExpr != null)
                    {
                        filterExpr = filterTimeExpr;
                    }
                }
            }
            if (filterExpr != null)
            {
                var lambdaQuery = Expression.Lambda<Func<WeatherInfo, bool>>
                            (filterExpr, new ParameterExpression[] { expression });
                query = query.Where(lambdaQuery);
            }
            query = query.Skip(offset).Take(size);

            return await query.ToListAsync();
        }

      
        private Expression GetRangeDateOnlyExpressionForField(FilterDTO<RangeDateFilterDTO> filter, ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, filter.Field);
            switch (filter.MatchMode)
            {
                case DTO.Enums.FilterMatchMode.Equals:
                {
                    return GetRangeDateOnlyConstantExpression(filter.Value, property);
                }
                default:
                {
                    return null;
                }
            }
        }

        private Expression GetTimeOnlyExpressionForField(FilterDTO<TimeOnly> filter, ParameterExpression parameter)
        {
            var property = Expression.Property(parameter, filter.Field);
            switch (filter.MatchMode)
            {
                case DTO.Enums.FilterMatchMode.Equals:
                    {
                        return GetTimeOnlyConstantExpression(filter, property);
                    }
                default:
                    {
                        return null;
                    }
            }
        }

        private Expression GetRangeDateOnlyConstantExpression(RangeDateFilterDTO dateOnly, Expression property)
        {
            var constantFrom = Expression.Constant(new DateTime(dateOnly.From.Year, dateOnly.From.Month,
                dateOnly.From.Day));

            var date = Expression.PropertyOrField(property, "Date");

            var gte = Expression.GreaterThanOrEqual(date, constantFrom);

            if (dateOnly.To.HasValue)
            {
                var constantTo = Expression.Constant(new DateTime(dateOnly.To.Value.Year, dateOnly.To.Value.Month,
                dateOnly.To.Value.Day));
                var lte = Expression.LessThanOrEqual(date, constantTo);

                return Expression.And(gte, lte);
            }
            return gte;
        }

        private Expression GetTimeOnlyConstantExpression(FilterDTO<TimeOnly> dateOnly, Expression property)
        {
            var constantHour = Expression.Constant(dateOnly.Value.Hour);
            var constantMinute = Expression.Constant(dateOnly.Value.Minute);
            var constantSecond = Expression.Constant(dateOnly.Value.Second);

            var hour = Expression.PropertyOrField(property, "Hour");
            var minute = Expression.PropertyOrField(property, "Minute");
            var second = Expression.PropertyOrField(property, "Second");

            var hourEqual = Expression.Equal(hour, constantHour);
            var minuteEqual = Expression.Equal(minute, constantMinute);
            var secondEqual = Expression.Equal(second, constantSecond);

            var and1 = Expression.And(hourEqual, minuteEqual);
            var and2 = Expression.And(and1, secondEqual);
           
            return Expression.And(and1, and2);
        }

        public async Task<int> GetTotalRecordsCount(List<FilterDTO<RangeDateFilterDTO>> dateOnlyFilters = null,
            IEnumerable<FilterDTO<TimeOnly>> timeFilters = null)
        {
            IQueryable<WeatherInfo> query = _dataContext.WeatherInfos.AsQueryable();

            Expression filterDateExpr = null;
            Expression filterTimeExpr = null;
            Expression filterExpr = null;
            var expression = Expression.Parameter(query.ElementType, "e");
            if (dateOnlyFilters != null)
            {
                foreach (var dateOnlyFilter in dateOnlyFilters)
                {
                    filterDateExpr = GetRangeDateOnlyExpressionForField(dateOnlyFilter, expression);
                }
                if (filterDateExpr != null)
                {
                    filterExpr = filterDateExpr;
                }
            }
            if (timeFilters != null)
            {
                foreach (var timeFilter in timeFilters)
                {
                    filterTimeExpr = GetTimeOnlyExpressionForField(timeFilter, expression);
                }
                if (filterExpr != null && filterTimeExpr != null)
                {
                    filterExpr = Expression.And(filterExpr, filterTimeExpr);
                }
                else
                {
                    if (filterTimeExpr != null)
                    {
                        filterExpr = filterTimeExpr;
                    }
                }
            }
            if (filterExpr != null)
            {
                var lambdaQuery = Expression.Lambda<Func<WeatherInfo, bool>>
                            (filterExpr, new ParameterExpression[] { expression });
                query = query.Where(lambdaQuery);
            }
         
            return await query.CountAsync();
        }

        public async Task<IEnumerable<WeatherInfo>> ImportRange(IEnumerable<WeatherInfo> weatherInfos)
        {
            await _dataContext.WeatherInfos.AddRangeAsync(weatherInfos);
            await _dataContext.SaveChangesAsync();
            return weatherInfos;
        }
    }
}
