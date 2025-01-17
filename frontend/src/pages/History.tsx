import React, { useEffect, useState } from "react";
import NavBar from "../micro-components/navbar.tsx";
import { Flex, Heading, Text } from "@chakra-ui/react";
import { useSelector } from "react-redux";
import { useColorMode } from "../components/ui/color-mode.jsx";
import { GATEWAY_PORT } from "../environment-variables.ts";
import { get } from "../utils/httpRequests.ts";

const History1 = () => {
  const { toggleColorMode } = useColorMode();
  const token = useSelector((state): any => state.user.token);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    toggleColorMode();
    const fetchOrders = async () => {
      try {
        const data = await get(GATEWAY_PORT, "orders/history", token);
        if (Array.isArray(data)) {
          setOrders(data);
        }
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };
    fetchOrders();
  }, []);
  return (
    <React.Fragment>
      <NavBar />
      <Flex justify="center" mt="10">
        <Flex width="60%" direction="column" gap="5" mb="10">
          {orders.map((o) => (
            <Flex
              key={o.orderId}
              p="5"
              shadow="lg"
              rounded="2xl"
              direction="column"
            >
              <Heading>Order #{orders.length}</Heading>
              <Flex direction="column" mt={2} gap={2}>
                <Flex direction="column">
                  <Flex align="center" gap="2">
                    <span class="material-symbols-outlined">
                      calendar_month
                    </span>
                    <Text>{new Date(o.date).toLocaleDateString("ro-RO")}</Text>
                  </Flex>
                  <Flex align="center" gap="2">
                    <span class="material-symbols-outlined">payments</span>
                    <Text>{parseFloat(o.price.toFixed(2))} EUR</Text>
                  </Flex>
                </Flex>
                <Flex direction="column">
                  <Flex align="center" gap="2">
                    <Heading>Products</Heading>
                  </Flex>
                  {o.products.map((p) => (
                    <Flex key={p.id} justify="space-between">
                      <Flex align="center" gap="2">
                        <span class="material-symbols-outlined">
                          shopping_cart
                        </span>
                        <Text> {p.quantity} X {p.name}</Text>
                      </Flex>
                      <Flex align="center" gap="2">
                        <span class="material-symbols-outlined">payments</span>
                        <Text>{p.price} EUR / unit</Text>
                      </Flex>
                    </Flex>
                  ))}
                </Flex>
              </Flex>
            </Flex>
          ))}
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

export default History1;
