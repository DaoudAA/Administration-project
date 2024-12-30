package tn.enit.crm.TicketingService.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import tn.enit.crm.TicketingService.model.GeneralTicket;
import tn.enit.crm.TicketingService.model.OrderTicket;
import tn.enit.crm.TicketingService.model.Ticket;
import tn.enit.crm.TicketingService.service.TicketService;

@RestController
@RequestMapping("/api/v1/tickets")
public class TicketController {
    @Autowired
    private TicketService ticketService;

    @GetMapping
    public ResponseEntity<List<Ticket>> getAllTickets() {
        return ResponseEntity.ok(ticketService.getAllTickets());
    }

    @GetMapping("/{id}")
    public ResponseEntity<Ticket> getTicketById(@PathVariable Long id) {
        Ticket ticket = ticketService.getTicketById(id);
        if (ticket == null) {
            return ResponseEntity.notFound().build();
        }
        return ResponseEntity.ok(ticket);
    }

    @PostMapping("/order")
    public ResponseEntity<Ticket> createOrderTicket(@RequestBody OrderTicket orderTicket) {
        Ticket savedTicket = ticketService.createTicket(orderTicket);
        // Check if the ticket is an OrderTicket before publishing
        ticketService.publishOrderTicketEvent((OrderTicket) savedTicket);
        return ResponseEntity.ok(savedTicket);
    }

    @PostMapping("/general")
    public ResponseEntity<Ticket> createGeneralTicket(@RequestBody GeneralTicket generalTicket) {
        return ResponseEntity.ok(ticketService.createTicket(generalTicket));
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteTicket(@PathVariable Long id) {
        ticketService.deleteTicket(id);
        return ResponseEntity.noContent().build();
    }
}

