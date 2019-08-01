//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Text;
//using System.Web;
//using ProjectLibrary.Database;
//using TeamplateHotel.Controllers;
//using TeamplateHotel.Models;

//namespace TeamplateHotel.Handler
//{
//    public class SendEmail
//    {
//        public static bool SendMailBookingOnline(BookRoom booking, string messages)
//        {
//            using (var db = new MyDbDataContext())
//            {
//                try
//                {
//                    //Chi tiết khách sạn.


//                    DetailHotel detailHotel = CommentController.DetailHotel(booking.HotelId);
//                    var configPayment = db.ConfigPayments.FirstOrDefault();

//                    var sendTour =
//                        "<!DOCTYPE html><html xmlns='http://www.w3.org/1999/xhtml'><head></head><body><div style='font-family: sans-serif; width: 680px; margin: 10px auto;font-size: 15px; line-height: 18px;'>" +
//                        "<div style='width: 100%; float: left; background: #f4f4f4; padding: 15px;'><div style='height: 40px; width: 100%; float: left;'><h3 style='font-size: 25px; margin:0px; width: 70%; float: left; color: #246fc1;'>"+detailHotel.HotelName+"</h3>" +
//                        "<a href='"+detailHotel.Website+"' style='  display: block;width: 29%;float: right;line-height: 34px;color: #555;text-align: right;font-size: 0.9em;color: #246fc1;'>Go to website</a></div>" +
//                        "<div style='height: 250px; width: 100%; float: left'><img src='https://lh4.googleusercontent.com/-Bq8Wr8xR2Po/VV63lB_x4WI/AAAAAAAAANg/O7pUtLMg8VA/w700-h250-no/banner-demo.png' width='680'/></div>" +
//                        "<div style='width: 100%; float: left'><h4 style='  color: #246fc1;margin: 15px 0px 0px;'>Reservation Confimation</h4><p>Dear "+booking.Gender+"."+booking.FirstName+",</p>" +
//                        "<p style='line-height: 22px;'>Thank you for choosing " + detailHotel.HotelName + " as your home away from home. It is our pleasure to confirm your reservation. Please don't hesitate to contact us with any changes.</p>" +
//                        "<div style='width: 500px; margin: 10px auto; font-size: 0.85em;'>" +
//                        "<h4 style='width: 100%;float: left;margin: 10px 0px;background: #246fc1;height: 25px;line-height: 25px;text-align: center;color: #fff;font-size: 0.9em;'>RESERVATION DETAILS</h4>" +
//                        "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Booking code</div><div style='width: 64%;; float: right;'>"+booking.Code+"</div></div>" +
//                        "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Booking Date</div><div style='width: 64%;; float: right;'>0"+booking.DateBook+"</div></div>" +
//                        "<div style='width: 100%; float: left; margin-bottom: 5px'><div style='width: 35%;; float: left'>Room type</div><div style='width: 64%;; float: right;'>";
//                    sendTour = booking.DetailBookingRooms.Aggregate(sendTour,
//                        (current, item) => current + (item.Room.TitleRoom + ": " + item.NumberRoom + ", "));
//                    sendTour += "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Total room rate</div><div style='width: 64%;; float: right;'>" +
//                                booking.MoneyRoomRate.ToString("N") + " USD</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>VAT (" +
//                                configPayment.VAT + "%)</div><div style='width: 64%;; float: right;'>" +
//                                booking.MoneyTAX.ToString("N") + " USD</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Net room payment</div><div style='width: 64%;; float: right;'>" +
//                                booking.TotalMoney.ToString("N") + " USD</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Package Total</div><div style='width: 64%;; float: right;'>" +
//                                booking.MoneyService.ToString("N") + " USD</div></div>";
//                    if (booking.PaymentOnline)
//                    {
//                        sendTour +=
//                            "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Payment Now</div><div style='width: 64%;; float: right;'>" +
//                            booking.PaymentNow.ToString("N") + " USD</div></div>";
//                    }
//                    sendTour +=
//                        "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Balance Due on Arrival</div><div style='width: 64%;; float: right;'>" +
//                        booking.PaymentArrive.ToString("N") + " USD</div></div>";
//                    if (booking.PaymentOnline)
//                    {
//                        sendTour +=
//                            "<div style='width: 100%; float: left; margin-bottom: 7px; font-weight: bold'><div style='width: 35%;; float: left'>Transaction status</div><div style='width: 64%;; float: right;'>" +
//                            messages + "</div></div>";
//                    }
//                    sendTour+="<h4 style='width: 100%;float: left;margin: 10px 0px;background: #246fc1;height: 25px;line-height: 25px;text-align: center;color: #fff;font-size: 0.9em;'>GUEST DETAILS</h4>" +
//                                "<div style='width: 100%; float: left'><div style='width: 35%;; float: left;margin-bottom: 7px;'>Guest Name</div><div style='width: 64%;; float: right;'>" +
//                                booking.FirstName + " " + booking.LastName + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Email Address</div><div style='width: 64%;; float: right;'>" +
//                                booking.Email + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Address</div><div style='width: 64%;; float: right;'>" +
//                                booking.Address + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Phone number</div><div style='width: 64%;; float: right;'>" +
//                                booking.Phone + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Country</div><div style='width: 64%;; float: right;'>" +
//                                booking.Country + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Smoking</div><div style='width: 64%;; float: right;'>";
//                    sendTour += booking.Smoking ? "Yes" : "No";
//                    sendTour += "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Messages</div><div style='width: 64%;; float: right;'>" +
//                                booking.OrtherRequest + "</div></div>" +
//                                "<h4 style='width: 100%;float: left;margin: 10px 0px;background: #246fc1;height: 25px;line-height: 25px;text-align: center;color: #fff;font-size: 0.9em;'>Arrival Details</h4>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Arrive with flight number</div><div style='width: 64%;; float: right;'>" +
//                                booking.ArrivalFlight + "</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Arrive Time</div><div style='width: 64%;; float: right;'>" +
//                                booking.ArrivalTime + "</div></div>" +
//                                "<h4 style='width: 100%;float: left;margin: 10px 0px;background: #246fc1;height: 25px;line-height: 25px;text-align: center;color: #fff;font-size: 0.9em;'>Hotel Policy</h4>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left;'>Cancellation</div><div style='width: 64%;; float: right;'>Cancellation without panalty is required by 4:00 PM EST on Wednesday, November 18,2009</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Check-in Time</div><div style='width: 64%;; float: right;'>3h00 AM</div></div>" +
//                                "<div style='width: 100%; float: left; margin-bottom: 7px;'><div style='width: 35%;; float: left'>Check-out Time</div><div style='width: 64%;; float: right;'>12h00 noon</div></div>" +
//                                "</div><div style='width: 100%; float: left; margin-bottom: 7px;'>" +
//                                "<p style='line-height: 22px;'>We are offfering special rates to upgrade your room. Reservation contact info: <a href='mailto:" +
//                                booking.Email + "'>" + booking.Email + "</a> or " + detailHotel.TelHotel + ".</p>" +
//                                "<p style='line-height: 22px;'>your reservation is guaranteed for late arrival. Should you wish to cancel, please do so two days prior to your arrival to avoid the one night's room charge for late cancellation. " +
//                                ". If we can assist you in making advance reservations at our restaurants, Henrietta's Table and Rialto, we are happy to help. Please feel free to contact us directly at welcoming you to the " +
//                                detailHotel.HotelName +
//                                "</p><strong>best regards,</strong></div></div></div></div></body></html>";

//                    var mail = db.MailConfigs.FirstOrDefault();
//                    if (mail != null)
//                    {
//                        //gui mail ve cong ty
//                        var mailMsg = new MailMessage
//                                          {From = new MailAddress(booking.Email, booking.FirstName, Encoding.UTF8)};
//                        mailMsg.To.Add(detailHotel.EmailHotel);
//                        mailMsg.ReplyToList.Add(new MailAddress(booking.Email, booking.FirstName, Encoding.UTF8));
//                        mailMsg.Subject = detailHotel.HotelName + "(" + booking.Code + ") - Booking room" +
//                                          " of " + booking.FirstName + " From " + booking.Country;
//                        mailMsg.IsBodyHtml = true;
//                        mailMsg.BodyEncoding = Encoding.UTF8;
//                        mailMsg.Body = sendTour;
//                        mailMsg.Priority = MailPriority.Normal;
//                        // Smtp configuration
//                        var client = new SmtpClient();

//                        client.Credentials = new NetworkCredential(mail.NameEmail, mail.PasswordEmail);
//                        client.Port = int.Parse(mail.Port);
//                        client.Host = mail.Host;
//                        client.EnableSsl = true;
//                        client.Send(mailMsg);
//                        //gui mail cho khach hang
//                        var mailMsgKhach = new MailMessage
//                                               {
//                                                   From =
//                                                       new MailAddress(detailHotel.EmailHotel, detailHotel.HotelName,
//                                                                       Encoding.UTF8)
//                                               };
//                        mailMsgKhach.To.Add(booking.Email);
//                        mailMsgKhach.ReplyToList.Add(new MailAddress(detailHotel.EmailHotel, detailHotel.HotelName,
//                                                                     Encoding.UTF8));
//                        mailMsgKhach.Subject = "Booking Room from " + detailHotel.HotelName;
//                        mailMsgKhach.IsBodyHtml = true;
//                        mailMsgKhach.BodyEncoding = Encoding.UTF8;
//                        mailMsgKhach.Body = sendTour;
//                        mailMsgKhach.Priority = MailPriority.Normal;
//                        var clientKhach = new SmtpClient
//                                              {
//                                                  Credentials =
//                                                      new NetworkCredential(mail.NameEmail, mail.PasswordEmail),
//                                                  Port = int.Parse(mail.Port),
//                                                  Host = mail.Host,
//                                                  EnableSsl = true
//                                              };
//                        clientKhach.Send(mailMsgKhach);
//                    }
//                }
//                catch (Exception exception)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }
//    }
//}