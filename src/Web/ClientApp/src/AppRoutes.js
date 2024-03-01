import { Counter } from "./components/Counter";
import { FeministOverview } from "./components/FeministOverview";
import { CollectiveOverview } from "./components/CollectiveOverview";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/overview',
    element: <FeministOverview/>
  },
  {
    path: '/collective',
    element: <CollectiveOverview />
  }
];

export default AppRoutes;
