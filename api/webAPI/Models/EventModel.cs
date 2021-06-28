﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class EventModel
    {

        public static List<EventDTO> GetEvents(ArvinoDbContext db)
        {
            return db.RV_Event.Include(x => x.RV_EventCategory)
                                .Select(e => new EventDTO()
                                {
                                    eventId = e.eventId,
                                    eventName = e.eventName,
                                    content = e.content,
                                    price = e.price,
                                    participantsAmount = e.participantsAmount,
                                    eventDate = e.eventDate,
                                    startTime = e.startTime,
                                    eventImgPath = e.eventImgPath,
                                    categoryId = e.categoryId ?? 0,
                                    wineryId = e.wineryId ?? 0,
                                    ticketsPurchased = e.ticketsPurchased ?? 0,
                                    categoryName = db.RV_EventCategory.FirstOrDefault(c => c.categoryId == e.categoryId).categoryName,
                                    wineryName = db.RV_Winery.FirstOrDefault(c => c.wineryId == e.wineryId).wineryName,
                                    wineryImage = db.RV_Winery.FirstOrDefault(c => c.wineryId == e.wineryId).IconImgPath,
                                    likes = db.RV_LikesUsers
                                                .Where(i => i.entityType == 1 && i.entityId == e.eventId)
                                                    .Select(dt => new LikesDTO()
                                                    {
                                                        likeId=dt.ID,
                                                        userName = db.RV_User.FirstOrDefault(w => w.email == dt.email).Name,
                                                        userImage = db.RV_User.FirstOrDefault(w => w.email == dt.email).picture

                                                    }).ToList()
                                    //ticketsLeft = e.participantsAmount - e.ticketsPurchased

                                }).ToList();
        }

        public static List<EventDTO> GetEventsByWinery(int wineryId, ArvinoDbContext db)
        {
            return db.RV_Event.Where(i => i.wineryId == wineryId)
                .Include(x => x.RV_EventCategory)
                .Select(e => new EventDTO()
                {
                    eventId = e.eventId,
                    eventName = e.eventName,
                    content = e.content,
                    price = e.price,
                    participantsAmount = e.participantsAmount,
                    eventDate = e.eventDate,
                    startTime = e.startTime,
                    eventImgPath = e.eventImgPath,
                    categoryId = e.categoryId ?? 0,
                    wineryId = e.wineryId ?? 0,
                    ticketsPurchased = e.ticketsPurchased ?? 0,
                    categoryName = db.RV_EventCategory.FirstOrDefault(c => c.categoryId == e.categoryId).categoryName
                }).ToList();
        }
    }
}