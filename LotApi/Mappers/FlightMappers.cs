﻿using LotApi.Dto;
using LotApi.Models;

namespace LotApi.Mappers
{
    public static class FlightMappers
    {
        public static FlightDto ToFlightDto(this Flight flightModel)
        {
            return new FlightDto
            {
                FlightNumber = flightModel.FlightNumber,
                DepartureDate = flightModel.DepartureDate,
                DepartureLocation = flightModel.DepartureLocation,
                ArrivalLocation = flightModel.ArrivalLocation,
                AircraftType = flightModel.AircraftType,
            };
        }

        public static Flight ToFlightFromCreateDto(this CreateFlightRequestDto flightDto)
        {
            return new Flight
            {
                FlightNumber = flightDto.FlightNumber,
                DepartureDate = flightDto.DepartureDate,
                DepartureLocation = flightDto.DepartureLocation,
                ArrivalLocation = flightDto.ArrivalLocation,
                AircraftType = flightDto.AircraftType,
            };
        }
    }
}
