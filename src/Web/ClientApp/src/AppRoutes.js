import { Counter } from "./components/Counter";
import { FeministOverview } from "./components/FeministOverview";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/overview',
    element: <FeministOverview/>
  }
];

export default AppRoutes;
