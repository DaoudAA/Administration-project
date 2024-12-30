package tn.enit.crm.TicketingService.service;

import org.springframework.stereotype.Service;

import tn.enit.crm.TicketingService.model.OrderTicket;
import tn.enit.crm.TicketingService.model.Ticket;
import tn.enit.crm.TicketingService.repository.TicketRepository;
import io.dapr.client.DaprClient;
import io.dapr.client.DaprClientBuilder;

import java.util.List;

@Service
public class TicketService {

    private final TicketRepository ticketRepository;

    public TicketService(TicketRepository ticketRepository) {
        this.ticketRepository = ticketRepository;
    }

    public List<Ticket> getAllTickets() {
        return ticketRepository.findAll();
    }

    public Ticket getTicketById(Long id) {
        return ticketRepository.findById(id).orElse(null);
    }

    public Ticket createTicket(Ticket ticket) {
        return ticketRepository.save(ticket);
    }
    public void publishOrderTicketEvent(OrderTicket orderTicket) {
        DaprClient daprClient = new DaprClientBuilder().build();
        daprClient.publishEvent("orderpubsub", "order-tickets", orderTicket).block();
        System.out.println("Published OrderTicket: " + orderTicket.getId());
    }
    public void deleteTicket(Long id) {
        ticketRepository.deleteById(id);
    }
}